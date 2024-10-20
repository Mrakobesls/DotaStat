// using Patch.Business.Services;
//
// namespace Patch.Observer;
//
// public class Worker : BackgroundService
// {
//     private readonly ILogger<Worker> _logger;
//     private readonly IPatchService _patchService;
//
//     public Worker(ILogger<Worker> logger, IPatchService patchService)
//     {
//         _logger = logger;
//         _patchService = patchService;
//     }
//
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         await _patchService.EnsurePatchHistory();
//     }
// }
