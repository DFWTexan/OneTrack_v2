using DataModel.Response;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;

namespace OneTrak_v2.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IConfiguration _config;
        private readonly AppDataContext _db;
        private readonly IUtilityHelpService _utilityService;

        private readonly string? _attachmentLocation;

        public DocumentService(IConfiguration config, AppDataContext db, IUtilityHelpService utilityHelpService)
        {
            _config = config;
            _db = db;
            _utilityService = utilityHelpService;

            // Retrieve the current environment setting
            string environment = _config.GetValue<string>("Environment") ?? "DVLP";

            // Construct the keys for accessing environment-specific settings
            string attachmentLocKey = $"EnvironmentSettings:{environment}:AttachmentLoc";

            // Retrieve the values based on the constructed keys
            _attachmentLocation = _config.GetValue<string>(attachmentLocKey);
        }

        public async Task<ReturnResult> Upload(string vFilePathUri, Stream vStream)
        {
            try
            {
                if (vStream == null)
                {
                    _utilityService.LogError("vStream is NULL.", "Server Error - Please Contact Support [REF# DOCS-3569-92418].", new { }, null);

                    return new ReturnResult
                    {
                        StatusCode = 400,
                        Success = false
                    };
                }

                string filePath = vFilePathUri != null ? vFilePathUri : _attachmentLocation;

                // Ensure the directory exists before trying to create a file
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Use Path.Combine to ensure the path is correctly formatted
                string fullPath = Path.Combine(directoryPath, Path.GetFileName(filePath));

                using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await vStream.CopyToAsync(fileStream);
                }

                return new ReturnResult
                {
                    StatusCode = 200,
                    Success = true,
                    ObjData = new { Message = "File Upload Successful" }
                };
            }
            catch (Exception ex)
            {
                _utilityService.LogError(ex.Message, "Server Error - Please Contact Support [REF# DOCS-3509-93312].", new { }, null);

                return new ReturnResult
                {
                    StatusCode = 500,
                    Success = false,
                    ErrMessage = "Server Error - Please Contact Support [REF# DOCS-3509-93312]."
                };
            }
        }

        public async Task<ReturnResult> Delete(string? vFilePathUri = null, string? vFilename = null)
        {
            var result = new ReturnResult();
            try
            {
                string filePath = vFilePathUri != null ? vFilePathUri : _attachmentLocation;

                string fullPath = Path.Combine(filePath, vFilename);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);

                    result.StatusCode = 200;
                    result.Success = true;
                    result.ObjData = new { Message = "File Deleted Successfully" };
                }
                else
                {
                    result.StatusCode = 404;
                    result.Success = false;
                    result.ErrMessage = "File Not Found";
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ErrMessage = "Server Error - Please Contact Support [REF# DOCS-3509-93412].";

                _utilityService.LogError(ex.Message, "Server Error - Please Contact Support [REF# DOCS-3509-93413].", new { }, null);
            }

            return await Task.FromResult(result);
        }
    }
}
