using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio_backend.Application.UseCase.V1.PedidosOperation.Queries.GetById
{
    public class GetPedidoByIdValidation : AbstractValidator<GetPedidoById>
    {
        public GetPedidoByIdValidation() 
        {
            RuleFor(x => x.Id)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("'Id' can't be null")
               .MaximumLength(255)
               .WithMessage("'Id' can only contain 255 characters");
        }
    }
    
}
