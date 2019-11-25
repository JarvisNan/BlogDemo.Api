using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.DB.Resources
{
    public class PostResourceValidator : AbstractValidator<PostResource>
    {
        public PostResourceValidator()
        {
            RuleFor(_ => _.Author)
                .NotNull()  //不能为空
                .WithName("作者")
                .WithMessage("{PropertyName}是必填的")
                .MaximumLength(20)
                .WithMessage("{PropertyName}的最大长度是｛MaxLength｝");
        }
    }
}
