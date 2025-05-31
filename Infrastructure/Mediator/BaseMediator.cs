using System.Reflection;
using Application.Interfaces.Mediator;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Mediator;

public class BaseMediator : IMediator
{
    private readonly IServiceProvider      _serviceProvider;
    private readonly ILogger<BaseMediator> _logger;

    public BaseMediator(IServiceProvider serviceProvider,
        ILogger<BaseMediator>            logger)
    {
        _serviceProvider = serviceProvider;
        _logger          = logger;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            _logger.LogError("Handler of type {HandlerType} not found", handlerType);
            throw new InvalidOperationException($"Handler for request type {request.GetType()} not found.");
        }

        var method = handlerType.GetMethod("HandleAsync");
        if (method == null)
        {
            _logger.LogError("Method HandleAsync not found on handler type {HandlerType}", handlerType);
            throw new InvalidOperationException($"HandleAsync method not found on handler type {handlerType}.");
        }

        try
        {
            var task = (Task<TResponse>)method.Invoke(handler, [request])!;
            return await task;
        }
        catch (TargetInvocationException ex) when (ex.InnerException != null)
        {
            throw ex.InnerException;
        }
    }
}