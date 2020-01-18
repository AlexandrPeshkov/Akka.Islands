using IslandRouter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IslandRouter
{
    [Route("api/[controller]")]
    [ApiController]
    public class IslandRouterController : ControllerBase
    {
        private readonly IslandRouterService _islandRouterService;
        public IslandRouterController(IslandRouterService islandRouterService)
        {
            _islandRouterService = islandRouterService;
        }

        /// <summary>
        /// Запустить ГА
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(Start))]
        public async Task<IActionResult> Start()
        {
            int islandCount = await _islandRouterService.ActiveIslands();
            _islandRouterService.StartGeneticAlgorithm();
            return Ok($"Starting with {islandCount}");
        }
    }
}
