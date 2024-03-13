using System.Collections.Generic;
using System.Threading.Tasks;
using CMMMobileMaui.API.Common;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Requests;
using CMMMobileMaui.API.Contracts.v1.Requests.Device;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Controllers.v1
{
    public class DeviceController : BaseBLL, IDeviceController
    {
        public DeviceController(IAPIManage apiManage) : base(apiManage)
        {
            this.Name = "device";
        }

        public Task<InvokeReturn<CreateDeviceResponse>> Create(CreateDeviceRequest createDevice) =>
            PostAsyncResult<CreateDeviceResponse>(new QueryMethod().SetJsonObject(createDevice));

        public Task<InvokeReturn<bool>> CreateObserved(CreateObservedDeviceRequest createObservedDevice) =>
            PostAsyncResult<bool>(new QueryMethod().SetJsonObject(createObservedDevice));

        public Task<InvokeReturn<GetDeviceListResponse>> Get(GetDeviceByIDLangRequest getDeviceByID) =>
            GetAsyncResult<GetDeviceListResponse>(new QueryMethod().AddParams(getDeviceByID));

        public Task<InvokeReturn<IEnumerable<GetBranchLocationsResponse>>> GetBranchLocations() =>
            GetAsyncResult<IEnumerable<GetBranchLocationsResponse>>(new QueryMethod());

        public Task<InvokeReturn<GetDeviceListResponse>> GetById(GetByDeviceIDRequest getByDeviceID) =>
            GetAsyncResult<GetDeviceListResponse>(new QueryMethod().AddParams(getByDeviceID));

        public Task<InvokeReturn<IEnumerable<GetDeviceListResponse>>> GetByName(GetDeviceByNameRequest getDeviceByName) =>
            GetAsyncResult<IEnumerable<GetDeviceListResponse>>(new QueryMethod().AddParams(getDeviceByName));

        public Task<InvokeReturn<IEnumerable<GetDeviceDetailsResponse>>> GetDetail(GetDeviceDetailsRequest getDeviceDetails) =>
            GetAsyncResult<IEnumerable<GetDeviceDetailsResponse>>(new QueryMethod().AddParams(getDeviceDetails));

        public Task<InvokeReturn<IEnumerable<DeviceDictResponse>>> GetDict(GetDictRequest getDict) =>
            GetAsyncResult<IEnumerable<DeviceDictResponse>>(new QueryMethod().AddParams(getDict));

        public Task<InvokeReturn<IEnumerable<GetDirectoryFilesResponse>>> GetDirFiles(GetDirectoryFilesRequest getDirectoryFiles) =>
            GetAsyncResult<IEnumerable<GetDirectoryFilesResponse>>(new QueryMethod().AddParams(getDirectoryFiles));

        public Task<InvokeReturn<GetFileResponse>> GetFile(GetFileNameRequest getFile) =>
            GetAsyncResult<GetFileResponse>(new QueryMethod().AddParams(getFile));

        public Task<InvokeReturn<IEnumerable<GetHistoryDeviceLocationResponse>>> GetHistLoc(GetByDeviceIDRequest getByDeviceID) =>
            GetAsyncResult<IEnumerable<GetHistoryDeviceLocationResponse>>(new QueryMethod().AddParams(getByDeviceID));

        public Task<InvokeReturn<GetDeviceImageResposne>> GetImage(GetByDeviceIDRequest getByDeviceID) =>
            GetAsyncResult<GetDeviceImageResposne>(new QueryMethod().AddParams(getByDeviceID));

        public Task<InvokeReturn<IEnumerable<GetDeviceListResponse>>> GetList(GetDeviceListRequest getDeviceList) =>
            GetAsyncResult<IEnumerable<GetDeviceListResponse>>(new QueryMethod().AddParams(getDeviceList));

        public Task<InvokeReturn<GetDeviceLocationResponse>> GetLocation(GetByDeviceIDRequest getByDeviceID) =>
            GetAsyncResult<GetDeviceLocationResponse>(new QueryMethod().AddParams(getByDeviceID));

        public Task<InvokeReturn<IEnumerable<GetMainLocationResponse>>> GetMainLocation() =>
            GetAsyncResult<IEnumerable<GetMainLocationResponse>>(new QueryMethod());
        public Task<InvokeReturn<GetRostiMouldCalendarResponse>> GetMouldCal(GetByDeviceIDRequest getByDeviceID) =>
            GetAsyncResult<GetRostiMouldCalendarResponse>(new QueryMethod().AddParams(getByDeviceID));

        public Task<InvokeReturn<IEnumerable<GetObservedResponse>>> GetObserved(GetObservedRequest getObservedRequest) =>
            GetAsyncResult<IEnumerable<GetObservedResponse>>(new QueryMethod().AddParams(getObservedRequest));
        public Task<InvokeReturn<GetScannedLocationResponse>> GetScanLoc(GetScannedLocationRequest getScannedLocation) =>
            GetAsyncResult<GetScannedLocationResponse>(new QueryMethod().AddParams(getScannedLocation));

        public Task<InvokeReturn<IEnumerable<GetDeviceStateResponse>>> GetState(GetDeviceStateRequest getDeviceState) =>
            GetAsyncResult<IEnumerable<GetDeviceStateResponse>>(new QueryMethod().AddParams(getDeviceState));

        public Task<InvokeReturn<IEnumerable<GetDeviceSubLocationResponse>>> GetSubLoc(GetByDeviceLocationIDRequest getByDeviceLocationID) =>
            GetAsyncResult<IEnumerable<GetDeviceSubLocationResponse>>(new QueryMethod().AddParams(getByDeviceLocationID));

        public Task<InvokeReturn<IEnumerable<GetDeviceWarrantyResponse>>> GetWarranty(GetByDeviceIDRequest getByDeviceID) =>
            GetAsyncResult<IEnumerable<GetDeviceWarrantyResponse>>(new QueryMethod().AddParams(getByDeviceID));

        public Task<InvokeReturn<IEnumerable<GetDeviceListResponse>>> GetWithSet(GetDeviceWithSetRequest getDeviceWithSet) =>
            GetAsyncResult<IEnumerable<GetDeviceListResponse>>(new QueryMethod().AddParams(getDeviceWithSet));

        public Task<InvokeReturn<GetDeviceImageResposne>> UpdateImage(UpdateDeviceImageRequest postDeviceImage) =>
            PutAsyncResult<GetDeviceImageResposne>(new QueryMethod().SetJsonObject(postDeviceImage));

        public Task<InvokeReturn<bool>> UpdateLoc(UpdateDeviceLocationRequest updateDeviceLocation) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(updateDeviceLocation));

        public Task<InvokeReturn<bool>> UpdateMouldCal(UpdateMouldCalendarRequest updateMouldCalendar) =>
            PutAsyncResult<bool>(new QueryMethod().SetJsonObject(updateMouldCalendar));

        public Task<InvokeReturn<bool>> CreateUserObserved(CreateUserObservedDeviceRequest createUserObservedDevice) =>
            PostAsyncResult<bool>(new QueryMethod().SetJsonObject(createUserObservedDevice));

        public Task<InvokeReturn<IEnumerable<GetDeviceObservedCyclesResponse>>> GetObservedCycles(GetByDeviceIDRequest getByDeviceID) =>
            GetAsyncResult<IEnumerable<GetDeviceObservedCyclesResponse>>(new QueryMethod().AddParams(getByDeviceID));
    }
}
