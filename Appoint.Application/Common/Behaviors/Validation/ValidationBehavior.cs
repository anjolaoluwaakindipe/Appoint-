using ErrorOr;
using FluentValidation;
using MediatR;

namespace Appoint.Application.Common.Behaviors.Validation;

public class ValidationBahavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBahavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(_validator is null)
        {
            return await next();
        }
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult
            .Errors
            .ConvertAll(validationFailure => Error.Validation(code: validationFailure.PropertyName,
                description: validationFailure.ErrorMessage));

        return (dynamic)errors;
    }
}