﻿using API.Common.Exceptions;
using API.Features.Identity.Exceptions;
using API.Features.Identity.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ExceptionFilter()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(CreateUserException), HandleCreateUserException },
            { typeof(ChangePasswordException), HandleChangePasswordException },
        };
    }
    
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }
    
    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        if (context.Exception is BaseException)
        {
            HandleNetCoreTemplateException(context);
            return;
        }
        
        HandleUnknownException(context);
    }
    private void HandleCreateUserException(ExceptionContext context)
    {
        var exception = context.Exception as CreateUserException;


        var details = new ValidationProblemDetails(exception?.Errors)
        {
            Title = exception?.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        context.Result = new NotFoundResult();

        context.ExceptionHandled = true;
    }
    
    private void HandleNetCoreTemplateException(ExceptionContext context)
    {
        var exception = context.Exception as BaseException;

        var details = new ProblemDetails()
        {
            Title = exception?.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState);

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleChangePasswordException(ExceptionContext context)
    {
        var exception = context.Exception as ChangePasswordException;
        
        var details = new ValidationProblemDetails(exception?.Errors)
        {
            Title = exception?.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private static void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
        };
        
        details.Detail = $"{context.Exception.Message} {context.Exception.Source} {context.Exception.StackTrace}";

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}