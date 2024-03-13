using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests;
using CMMMobileMaui.API.Contracts.v1.Requests.Device;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IDeviceController
    {
        Task<InvokeReturn<IEnumerable<DeviceDictResponse>>> GetDict(GetDictRequest getDict);
        Task<InvokeReturn<IEnumerable<GetObservedResponse>>> GetObserved(GetObservedRequest getObservedRequest);
        Task<InvokeReturn<IEnumerable<GetDeviceDetailsResponse>>> GetDetail(GetDeviceDetailsRequest getDeviceDetails);
        Task<InvokeReturn<IEnumerable<GetDeviceStateResponse>>> GetState(GetDeviceStateRequest getDeviceState);
        Task<InvokeReturn<IEnumerable<GetDeviceListResponse>>> GetList(GetDeviceListRequest getDeviceList);
        Task<InvokeReturn<IEnumerable<GetBranchLocationsResponse>>> GetBranchLocations();
        Task<InvokeReturn<IEnumerable<GetDeviceListResponse>>> GetByName(GetDeviceByNameRequest getDeviceByName);
        Task<InvokeReturn<IEnumerable<GetDeviceListResponse>>> GetWithSet(GetDeviceWithSetRequest getDeviceWithSet);
        Task<InvokeReturn<GetDeviceListResponse>> Get(GetDeviceByIDLangRequest getDeviceByID);
        Task<InvokeReturn<GetDeviceImageResposne>> GetImage(GetByDeviceIDRequest getByDeviceID);
        Task<InvokeReturn<GetDeviceImageResposne>> UpdateImage(UpdateDeviceImageRequest postDeviceImage);
        Task<InvokeReturn<IEnumerable<GetDeviceWarrantyResponse>>> GetWarranty(GetByDeviceIDRequest getByDeviceID);
        Task<InvokeReturn<GetDeviceLocationResponse>> GetLocation(GetByDeviceIDRequest getByDeviceID);
        Task<InvokeReturn<IEnumerable<GetMainLocationResponse>>> GetMainLocation();
        Task<InvokeReturn<IEnumerable<GetHistoryDeviceLocationResponse>>> GetHistLoc(GetByDeviceIDRequest getByDeviceID);
        Task<InvokeReturn<IEnumerable<GetDeviceObservedCyclesResponse>>> GetObservedCycles(GetByDeviceIDRequest getByDeviceID);
        Task<InvokeReturn<CreateDeviceResponse>> Create(CreateDeviceRequest createDevice);
        Task<InvokeReturn<bool>> CreateObserved(CreateObservedDeviceRequest createObservedDevice);
        Task<InvokeReturn<GetDeviceListResponse>> GetById(GetByDeviceIDRequest getByDeviceID);
        Task<InvokeReturn<IEnumerable<GetDirectoryFilesResponse>>> GetDirFiles(GetDirectoryFilesRequest getDirectoryFiles);
        Task<InvokeReturn<GetFileResponse>> GetFile(GetFileNameRequest getFile);
        Task<InvokeReturn<bool>> UpdateLoc(UpdateDeviceLocationRequest updateDeviceLocation);
        Task<InvokeReturn<GetScannedLocationResponse>> GetScanLoc(GetScannedLocationRequest getScannedLocation);
        Task<InvokeReturn<IEnumerable<GetDeviceSubLocationResponse>>> GetSubLoc(GetByDeviceLocationIDRequest getByDeviceLocationID);
        Task<InvokeReturn<GetRostiMouldCalendarResponse>> GetMouldCal(GetByDeviceIDRequest getByDeviceID);
        Task<InvokeReturn<bool>> UpdateMouldCal(UpdateMouldCalendarRequest updateMouldCalendar);
        Task<InvokeReturn<bool>> CreateUserObserved(CreateUserObservedDeviceRequest createUserObservedDevice);

    }
}
