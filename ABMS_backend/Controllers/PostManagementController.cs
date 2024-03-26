﻿using ABMS_backend.DTO;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Validates;
using Microsoft.AspNetCore.Mvc;
using ABMS_backend.DTO.PostDTO;
using ABMS_backend.Models;

namespace ABMS_backend.Controllers
{
    [Route(Constants.REQUEST_MAPPING_PREFIX + Constants.VERSION_API_V1)]
    [ApiController]
    public class PostManagementController : ControllerBase
    {
        private IPostManagermentRepository _repository;

        public PostManagementController(IPostManagermentRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("post/createResident")]
        public ResponseData<string> CreateForResident([FromBody] PostForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createPostForResident(dto);
            return response;
        }

        [HttpPost("post/createReceptionist")]
        public ResponseData<string> CreateForReceptionist([FromBody] PostForInsertDTO dto)
        {
            ResponseData<string> response = _repository.createPostForReceptionist(dto);
            return response;
        }

        [HttpDelete("post/delete/{id}")]
        public ResponseData<string> Delete(String id)
        {
            ResponseData<string> response = _repository.deletePost(id);
            return response;
        }

        [HttpPut("post/update/{id}")]
        public ResponseData<string> Update(String id, [FromBody] PostForInsertDTO dto)
        {
            ResponseData<string> response = _repository.updatePost(id, dto);
            return response;
        }

        [HttpGet("post/get-all")]
        public ResponseData<List<Post>> GetAll([FromQuery] PostForSearchDTO dto)
        {
            ResponseData<List<Post>> response = _repository.getAllPost(dto);
            return response;
        }
        [HttpPost("markAsRead")]
        public IActionResult MarkAsRead(string accountId)
        {
            _repository.MarkNotificationsAsRead(accountId);
            return Ok();
        }

        [HttpGet("post/get-notification")]
        public IEnumerable<Post> GetNotifcation([FromQuery]string accountId, [FromQuery] bool onlyUnread,
            [FromQuery] int skip, [FromQuery] int take)
        {
            IEnumerable<Post> response = _repository.GetNotifications(accountId, onlyUnread, skip,take);
            return response;
        }

        [HttpGet("post/getPostId/{id}")]
        public ResponseData<Post> GetPostById(String id)
        {
            ResponseData<Post> response = _repository.getPostById(id);
            return response;
        }
    }
}
