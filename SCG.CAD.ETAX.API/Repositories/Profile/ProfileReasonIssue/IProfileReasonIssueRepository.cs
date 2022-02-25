namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IProfileReasonIssueRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ProfileReasonIssue param);
        Task<Response> UPDATE(ProfileReasonIssue param);
        Task<Response> DELETE(ProfileReasonIssue param);
    }
}
