﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalWorkDentistry.Helpers
{
    public static class AttributeManager
    {
        public static bool HasAttribute(AuthorizationFilterContext context, Type targetAttribute)
        {
            var hasAttribute = false;
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                hasAttribute = controllerActionDescriptor
                    .MethodInfo
                    .GetCustomAttributes(targetAttribute, false).Any();
            }

            return hasAttribute;
        }
    }
}
