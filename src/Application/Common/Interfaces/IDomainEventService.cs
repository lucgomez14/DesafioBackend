using desafio_backend.Domain.Common;
using System.Threading.Tasks;

namespace desafio_backend.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
