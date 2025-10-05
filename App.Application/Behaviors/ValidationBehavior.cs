using FluentValidation;
using FluentValidation.Results;
using MediatR;

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

            var validationErrors = _validators.Select(v => v.Validate(context)).SelectMany(v => v.Errors).Where(v => v != null).GroupBy(v => v.PropertyName, v => v.ErrorMessage, (propertyName, errorMessage) => new { Key = propertyName, Values = errorMessage.Distinct().ToArray() }).ToDictionary(v => v.Key, v => v.Values[0]);

            if (validationErrors.Any())
            {
                var errors = validationErrors.Select(v => new ValidationFailure { PropertyName = v.Value, ErrorCode = v.Key });
                throw new ValidationException(errors);
            }
            return await next(cancellationToken);
        }
    }
}
