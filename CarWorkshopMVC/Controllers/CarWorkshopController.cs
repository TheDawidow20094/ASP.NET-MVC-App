using AutoMapper;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetCarWorkshopByEncodedName;
using CarWorkshop.Application.CarWorkshopService.Commands;
using CarWorkshop.Application.CarWorkshopService.Queries.GetCarWorkshopServices;
using CarWorkshopMVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CarWorkshopMVC.Extensions.ControllerExtensions;

namespace CarWorkshopMVC.Controllers
{
    public class CarWorkshopController : Controller
    {
        private readonly IMediator _mediator;
        readonly IMapper _mapper;

        public CarWorkshopController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var carWorkshops = await _mediator.Send(new GetAllCarWorkshopsQuery());
            return View(carWorkshops);           
        }

        //[Authorize (Roles = "Owner")]//Jeżeli nie bedzie zalogowany to przekieruje go na logowanie + musi mieć odpwoiednią role
        public IActionResult Create()
        {
            return View();
        }

        [Route("CarWorkshop/{encodedWorkshopName}/WorkshopDetails")]
        public async Task<IActionResult> WorkshopDetails(string encodedWorkshopName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedWorkshopName));
            return View(dto);
        }

        [Route("CarWorkshop/{encodedWorkshopName}/EditWorkshop")]
        public async Task<IActionResult> EditWorkshop(string encodedWorkshopName)
        {
            var dto = await _mediator.Send(new GetCarWorkshopByEncodedNameQuery(encodedWorkshopName));
            if (dto.CanEdit == false)
            {
                return RedirectToAction("NoAccess", "Home");
            }
            EditCarWorkshopCommand model = _mapper.Map<EditCarWorkshopCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("CarWorkshop/{encodedWorkshopName}/EditWorkshop")]
        public async Task<IActionResult> EditWorkshop(string encodedWorkshopName, EditCarWorkshopCommand command)
        {
            if (ModelState.IsValid == false)// Sprawdzenie walidacji po atrubutach na obiekcie DTO
            {
                return View(command);
            }
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if (ModelState.IsValid == false)// Sprawdzenie walidacji po atrubutach na obiekcie DTO
            {
                return View(command);
            }
            await _mediator.Send(command);
            this.SetNotification(NotyficationTypes.Success, $"Created workshop {command.Name}");
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        [Route("CarWorkshop/CarWorkshopService")]
        public async Task<IActionResult> CreateCarWorkshopService(CreateCarWorkshopServiceCommand command)
        {
            if (ModelState.IsValid == false)// Sprawdzenie walidacji po atrubutach na obiekcie DTO
            {
                return BadRequest(ModelState);
            }
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("CarWorkshop/{encodedName}/CarWorkshopService")]
        public async Task<IActionResult> GetCarworkshopServices(string encodedName)
        {
            var data = await _mediator.Send(new GetCarworkshopServicesQuery() { EncodedName = encodedName });
            return Ok(data);
        }
    }
}
