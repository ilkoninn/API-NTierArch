using App.Business.DTOs.SettingDTOs;
using App.Business.Validators.Commons;
using FluentValidation;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Validators.SettingValidators
{
    public class UpdateSettingDTOValidator : BaseEntityValidator<UpdateSettingDTO>
    {
        public UpdateSettingDTOValidator()
        {
            RuleFor(x => x.Key)
                 .NotEmpty().WithMessage("Key is required.")
                 .MaximumLength(100).WithMessage("Key must not exceed 100 characters.");
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Value is required.")
                .MaximumLength(500).WithMessage("Value must not exceed 500 characters.");
        }
    }
}
