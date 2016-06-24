using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using AdAtTheRightTime.Tools;


namespace AdAtTheRightTime.Controllers
{
    public class DriveController : Controller
    {
        // GET: Drive
        public async Task<ActionResult> IndexAsync(CancellationToken cancellationToken)
            {
                var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).
                    AuthorizeAsync(cancellationToken);

                if (result.Credential != null)
                {
                    var service = new DriveService(new BaseClientService.Initializer
                    {
                        HttpClientInitializer = result.Credential,
                        ApplicationName = "ASP.NET MVC Sample"
                    });

                    // YOUR CODE SHOULD BE HERE..
                    // SAMPLE CODE:
                    var list = await service.Files.List().ExecuteAsync();
                    ViewBag.Message = "FILE COUNT IS: " + list.Items.Count();
                    return View();
                }
                else
                {
                    return new RedirectResult(result.RedirectUri);
                }
            }
        }
    }