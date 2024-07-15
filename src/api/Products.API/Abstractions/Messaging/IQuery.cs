using FluentResults;
using MediatR;

namespace Products.API.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;