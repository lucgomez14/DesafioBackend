using System;

namespace desafio_backend.Domain.Common;

public record struct AuditableEntity(DateTime Created, string CreatedBy, DateTime? LastModified, string LastModifiedBy) { }