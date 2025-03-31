using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Unity;

namespace API.Common
{
    public class ControllerActivator : IHttpControllerActivator
    {
        /// <summary>
        /// Instantiates a controller from the same scope create in OWIN pipeline.
        /// </summary>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var container = request.GetOwinContext().GetContainer();
            var controller = (IHttpController)container.Resolve(controllerType);
            return controller;
        }
    }
}