using ABMS_backend.DTO;
using ABMS_backend.DTO.ReportDTO;
using ABMS_backend.Models;
using ABMS_backend.Repositories;
using ABMS_backend.Utils.Exceptions;
using ABMS_backend.Utils.Validates;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ABMS_backend.Services
{
    public class ReportService : IReportRepository
    {
        private readonly abmsContext _abmsContext;

        public ReportService(abmsContext abmsContext)
        {
            _abmsContext = abmsContext;
        }

        public ResponseData<ReportDTO> buildingReport(string buildingId)
        {
            ReportDTO dto = new ReportDTO();
            dto.totalRooms = _abmsContext.Rooms.Where(x => x.BuildingId == buildingId && x.Status == (int)Constants.STATUS.ACTIVE).Count();
            dto.totalUtilities = _abmsContext.Utilities.Where(x => x.BuildingId == buildingId && x.Status == (int)Constants.STATUS.ACTIVE).Count();
            dto.totalPosts = _abmsContext.Posts.Where(x => x.BuildingId == buildingId && x.Status != (int)Constants.STATUS.IN_ACTIVE).Count();
            dto.totalUtilityRequests = _abmsContext.UtilitySchedules.Where(x => x.Room.BuildingId == buildingId && x.Status != (int)Constants.STATUS.IN_ACTIVE).Count();
            dto.totalParkingCardRequests = _abmsContext.ParkingCards.Where(x => x.Resident.Room.BuildingId == buildingId && x.Status != (int)Constants.STATUS.IN_ACTIVE).Count();
            dto.totalElevatorRequests = _abmsContext.Elevators.Where(x => x.Room.BuildingId == buildingId && x.Status != (int)Constants.STATUS.IN_ACTIVE).Count();
            dto.totalConstructionRequests = _abmsContext.Constructions.Where(x => x.Room.BuildingId == buildingId && x.Status != (int)Constants.STATUS.IN_ACTIVE).Count();
            dto.totalVisitorRequests = _abmsContext.Visitors.Where(x => x.Room.BuildingId == buildingId && x.Status != (int)Constants.STATUS.IN_ACTIVE).Count();
            return new ResponseData<ReportDTO>
            {
                Data = dto,
                StatusCode = HttpStatusCode.OK,
                ErrMsg = ErrorApp.SUCCESS.description
            };
        }
    }
}
