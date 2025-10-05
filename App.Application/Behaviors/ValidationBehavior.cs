using App.Application.Common.Concretes.Dtos;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace App.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next(cancellationToken);

            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = results.SelectMany(r => r.Errors)
                                  .Where(e => e is not null)
                                  .GroupBy(e => new { e.PropertyName, e.ErrorMessage })
                                  .Select(g => g.Key)
                                  .ToList();

            if (failures.Count == 0)
                return await next(cancellationToken);

            var errors = failures
                .Select(e => string.IsNullOrWhiteSpace(e.PropertyName)
                    ? e.ErrorMessage
                    : $"{e.PropertyName}: {e.ErrorMessage}")
                .ToList();

            return CreateFailure(errors, HttpStatusCode.BadRequest);
        }
        private static TResponse CreateFailure(List<string> errors, HttpStatusCode statusCode)
        {
            var respType = typeof(TResponse);

            if (respType == typeof(ServiceResult))
                return (TResponse)(object)ServiceResult.Fail(errors, statusCode);

            if (respType.IsGenericType && respType.GetGenericTypeDefinition() == typeof(ServiceResult<>))
            {
                var failMethod = respType.GetMethod(nameof(ServiceResult<object>.Fail), [typeof(List<string>), typeof(HttpStatusCode)]);

                if (failMethod is not null)
                {
                    var result = failMethod.Invoke(null, [errors, statusCode])!;
                    return (TResponse)result;
                }
            }

            throw new InvalidOperationException($"ValidationBehavior, TResponse type must be ServiceResult or ServiceResult<T> Current: {respType.FullName}");
        }
    }
}
