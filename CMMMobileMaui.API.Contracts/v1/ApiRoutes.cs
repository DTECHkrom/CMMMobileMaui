namespace CMMMobileMaui.API.Contracts.v1
{
    public class ApiRoutes
    {
        private const string root = "api";
        private const string version = "v1";

       // protected const string mainBase = root + "/" + version + "/[controller]";
        protected const string mainBase = root + "/" + version + "/";

        public class Other
        {
            public const string GetAppSett = mainBase + nameof(Other) + "/getappsett";
            public const string GetBranchLocations = mainBase + nameof(Other) + "/getbranchlocations";
        }

        public class Set
        {
            public const string GetIsSetUsed = mainBase + nameof(Set) + "/getissetused";
            public const string GetSets = mainBase + nameof(Set) + "/getsets";
            public const string GetSubSets = mainBase + nameof(Set) + "/getsubsets";
        }

        public class Act
        {
            public const string GetDict = mainBase + nameof(Act) + "/getwoactdict";
            public const string GetList = mainBase + nameof(Act) + "/getlist";
            public const string Create = mainBase + nameof(Act) + "/create";
            public const string Get = mainBase + nameof(Act) + "/get";
            public const string Delete = mainBase + nameof(Act) + "/delete";
        }

        public class ActPer
        {

            public const string Create = mainBase + nameof(ActPer) + "/create";
            public const string Get = mainBase + nameof(ActPer) + "/get";
            public const string Delete = mainBase + nameof(ActPer) + "/delete";
        }

        public class Identity
        {
            public const string Login = mainBase + nameof(Identity) + "/login";
        }

        public class Plan
        {
            public const string GetActs = mainBase + nameof(Plan) + "/getacts";
            public const string GetAct = mainBase + nameof(Plan) + "/getact";
            public const string CreateAct = mainBase + nameof(Plan) + "/createact";
        }

        public class Device
        {
            public const string GetDict = mainBase + nameof(Device) + "/getdict";
            public const string GetState = mainBase + nameof(Device) + "/getstate";
            public const string GetList = mainBase + nameof(Device) + "/getlist";
            public const string GetDetail = mainBase + nameof(Device) + "/getdetail";
            public const string Get = mainBase + nameof(Device) + "/get";
            public const string GetById = mainBase + nameof(Device) + "/getbyid";
            public const string GetByName = mainBase + nameof(Device) + "/getbyname";
            public const string GetWithSet = mainBase + nameof(Device) + "/getwithset";
            public const string GetImage = mainBase + nameof(Device) + "/getimage";
            public const string UpdateImage = mainBase + nameof(Device) + "/updateimage";
            public const string GetWarranty = mainBase + nameof(Device) + "/getwarranty";
            public const string GetLocation = mainBase + nameof(Device) + "/getlocation";
            public const string GetHistLocation = mainBase + nameof(Device) + "/gethistloc";
            public const string GetDirFiles = mainBase + nameof(Device) + "/getdirfiles";
            public const string GetFile = mainBase + nameof(Device) + "/getfile";
            public const string GetScanLoc = mainBase + nameof(Device) + "/getscanloc";
            public const string UpdateLocation = mainBase + nameof(Device) + "/updateloc";
            public const string GetSubLoc = mainBase + nameof(Device) + "/getsubloc";
            public const string GetMouldCal = mainBase + nameof(Device) + "/getmouldcal";
            public const string UpdateMoulCal = mainBase + nameof(Device) + "/updatemouldcal";
            public const string Create = mainBase + nameof(Device) + "/crate";
        }

        public class WO
        {
            public const string GetDict = mainBase + nameof(WO) + "/getdict";    
            public const string GetList = mainBase + nameof(WO) + "/getlist";
            public const string GetHist = mainBase + nameof(WO) + "/gethist";
            public const string GetFiles = mainBase + nameof(WO) + "/getfiles";
            public const string GetParts = mainBase + nameof(WO) + "/getparts";
            public const string GetFile = mainBase + nameof(WO) + "/getfile";
            public const string CreateFile = mainBase + nameof(WO) + "/createfile";
            public const string UpdateFile = mainBase + nameof(WO) + "/updatefile";
            public const string DeleteFile = mainBase + nameof(WO) + "/deletefile";
            public const string Close = mainBase + nameof(WO) + "/close";
            public const string Take = mainBase + nameof(WO) + "/take";
            public const string Create = mainBase + nameof(WO) + "/create";
            public const string Update = mainBase + nameof(WO) + "/update";
            public const string Get = mainBase + nameof(WO) + "/get";
        }

        public class Part
        {
            public const string GetDict = mainBase + nameof(Part) + "/getdict";
            public const string GetCat = mainBase + nameof(Part) + "/getcat";
            public const string GetDetail = mainBase + nameof(Part) + "/getdetail";
            public const string GetDetailShort = mainBase + nameof(Part) + "/getdetailshort";
            public const string GetChange = mainBase + nameof(Part) + "/getchange";
            public const string GetLocation = mainBase + nameof(Part) + "/getlocation";
            public const string GetWarehouseLocation = mainBase + nameof(Part) + "/getwarehouselocation";
            public const string UpdateTake = mainBase + nameof(Part) + "/take";
            public const string UpdateReturn = mainBase + nameof(Part) + "/return";
            public const string CreateOrder = mainBase + nameof(Part) + "/createorder";
            public const string GetNewPartNo = mainBase + nameof(Part) + "/getnewpartno";
            public const string UpdateImage = mainBase + nameof(Part) + "/updateimage";
            public const string UpdateLocation = mainBase + nameof(Part) + "/updatelocation";
            public const string GetStocktaking = mainBase + nameof(Part) + "/getstocktaking";
            public const string GetPartStocktaking = mainBase + nameof(Part) + "/getpartstocktaking";
            public const string Create = mainBase + nameof(Part) + "/create";
            public const string CreateStocktaking = mainBase + nameof(Part) + "/createstocktaking";
            public const string CreateStocktakingPart = mainBase + nameof(Part) + "/createstocktakingpart";
            public const string UpdateStocktaking = mainBase + nameof(Part) + "/updatestocktaking";
            public const string DeleteStocktaking = mainBase + nameof(Part) + "/deletestocktaking";
            public const string EndStocktaking = mainBase + nameof(Part) + "/endstocktaking";
            public const string DeleteStocktakingPart = mainBase + nameof(Part) + "/deletestocktakingpart";
            public const string GetByID = mainBase + nameof(Part) + "/getbyid";
            public const string GetStocktakingByID = mainBase + nameof(Part) + "/getstocktakingbyid";
            public const string GetInternalOrderByID = mainBase + nameof(Part) + "/getinternalorderbyid";
            public const string GetLeftStock = mainBase + nameof(Part) + "/getleftstock";
            public const string AddQuantity = mainBase + nameof(Part) + "/addquantity";
        }

        public class User
        {
            public const string GetList = mainBase + nameof(User) + "/getlist";
            public const string Get = mainBase + nameof(User) + "/get";
            public const string GetRight = mainBase + nameof(User) + "/getright";
            public const string GetLoginByPass = mainBase + nameof(User) + "/getloginbypass";
            public const string GetLoginByCode = mainBase + nameof(User) + "/getloginbycode";
            public const string UpdateCode = mainBase + nameof(User) + "/updatecode";
        }
    }
}
