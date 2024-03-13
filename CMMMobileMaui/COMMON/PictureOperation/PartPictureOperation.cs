using CMMMobileMaui.API.Contracts;

namespace CMMMobileMaui.COMMON.PictureOperation
{
    public class PartPictureOperation : IPictureOperation
    {
        public IPictureModel PictureModel => throw new NotImplementedException();

        public Task<bool> Delete(WOFile fileModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(WOFile fileModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Replace(WOFile fileModel)
        {
            throw new NotImplementedException();
        }

        public Task Select()
        {
            throw new NotImplementedException();
        }
    }
}
