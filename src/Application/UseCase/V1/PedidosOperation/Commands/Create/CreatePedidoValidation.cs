
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace desafio_backend.Application.UseCase.V1.PedidosOperation.Commands.Create
{
    public class CreatePedidoValidation : AbstractValidator<CreatePedidoCommand>
    {
        public CreatePedidoValidation()
        {
            RuleFor(x => x.CuentaCorriente)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("'CuentaCorriente' can't be null")
                .Matches("^[0-9]*$")
                .WithMessage("'CuentaCorriente' can only contain numbers")
                .MaximumLength(255)
                .WithMessage("'CuentaCorriente' exceeds the maximum of 255 characters");
            RuleFor(x => x.CodigoDeContratoInterno)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("'CodigoDeContratoInterno' can't be null")
                .Matches("^[0-9]*$")
                .WithMessage("'CodigoDeContratoInterno' can only contain numbers")
                .MaximumLength(255)
                .WithMessage("'CodigoDeContratoInterno' exceeds the maximum of 255 characters");
        }
    }
}
