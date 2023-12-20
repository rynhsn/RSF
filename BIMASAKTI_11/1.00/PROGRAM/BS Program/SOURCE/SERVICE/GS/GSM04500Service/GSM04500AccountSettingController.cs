using GSM04500Common;
using Microsoft.AspNetCore.Mvc;

namespace GSM04500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM04500AccountSettingController : ControllerBase, IGSM04500AccountSetting
{