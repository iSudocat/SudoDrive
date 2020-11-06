using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;

namespace Server.Controllers.Storage
{
    [Route("api/storage/file")]
    [ApiController]
    [NeedPermission(PermissionBank.StorageFileUploadBasic)]
    public class FileUploadConfirmedController : AbstractController
    {
        private IDatabaseService _databaseService;
        

        private readonly ILogger _logger;


        public FileUploadConfirmedController(IDatabaseService databaseService, ILogger<GlobalExceptionFilter> logger)
        {
            _databaseService = databaseService;
            _logger = logger;
        }

        
        [HttpPatch]
        public IActionResult RequestUpload([FromBody] FileUploadConfirmRequestModel requestRequestModel)
        {
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }

            var file = _databaseService.Files.FirstOrDefault(s => 
                s.Id == requestRequestModel.Id && 
                s.Guid == requestRequestModel.Guid &&
                s.Status == Models.Entities.File.FileStatus.Pending &&
                s.User == loginUser);

            if (file == null)
            {
                throw new ConfirmingFileNotFoundException();
            }

            file.Status = Models.Entities.File.FileStatus.Confirmed;
            // _databaseService.Files.Update(file)
            _databaseService.SaveChanges();

            return Ok(file.ToVo());
        }
    }
}
