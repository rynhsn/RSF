using System.Diagnostics;
using LMT03500Common;

namespace LMT03500Back;

public class LMT03500UpdateMeterCls
{
    private LoggerLMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public LMT03500UpdateMeterCls()
    {
        _logger = LoggerLMT03500.R_GetInstanceLogger();
        _activitySource = LMT03500Activity.R_GetInstanceActivitySource();
    }
}