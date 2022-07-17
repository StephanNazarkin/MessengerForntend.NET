﻿using MessengerFrontend.Filters;
using MessengerFrontend.Models.Messages;
using MessengerFrontend.Routes;
using MessengerFrontend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessengerFrontend.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageServiceAPI _messageServiceAPI;
        private string Token => HttpContext.Session.GetString("Token");

        public MessageController(IMessageServiceAPI messageServiceAPI)
        {
            _messageServiceAPI = messageServiceAPI;
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> Index(int id, int chatId)
        {
            var model = await _messageServiceAPI.GetMessage(id, Token);
            ViewBag.ChatId = chatId;

            return View(model);
        }

        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageCreateModel model)
        {
            bool response = await _messageServiceAPI.SendMessage(model, Token);

            return Redirect(string.Format(RoutesApp.Chat, model.ChatId));
        }

        [AuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> Edit(MessageUpdateModel model)
        {
            var response = await _messageServiceAPI.EditMessage(model, Token);

            return Redirect(string.Format(RoutesApp.Chat, model.ChatId));
        }

        [AuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> Delete(int id, int chatId)
        {
            var response = await _messageServiceAPI.DeleteMessage(id, Token);

            return Redirect(string.Format(RoutesApp.Chat, chatId));
        }
    }
}
