namespace CMMMobileMaui.API.Contracts.v1.Responses.User
{
    // public record GetUserRightResponse(bool WO_Add, bool WO_Edit, bool WO_Edit_Description, bool WO_Del, bool WO_Close, bool ACT_Add, bool ACT_Del, bool ACT_Edit_Description
    //   , bool PART_WO_take, bool PART_WO_Order, bool PART_Edit_State, bool WO_SET_AssignedPerson, bool PART_Give, bool MD_Add, bool MD_Edit, bool MD_Edit_Warranty, bool PART_Add);

    public class GetUserRightResponse
    {
        public short PersonID { get; set; }
        public bool WO_Add { get; set; }
        public bool WO_Edit { get; set; }
        public bool WO_Edit_Description { get; set; }
        public bool WO_Del { get; set; }
        public bool WO_Close { get; set; }
        public bool ACT_Add { get; set; }
        public bool ACT_Del { get; set; }
        public bool ACT_Edit_Description { get; set; }
        public bool PART_WO_Take { get; set; }
        public bool PART_WO_Order { get; set; }
        public bool PART_Edit_State { get; set; }
        public bool WO_SET_AssignedPerson { get; set; }
        public bool PART_Give { get; set; }
        public bool MD_Add { get; set; }
        public bool MD_Edit { get; set; }
        public bool MD_Edit_Warranty { get; set; }
        public bool PART_Add { get; set; }
        public bool MD_Add_ForceCycle { get; set; }
    }
}
