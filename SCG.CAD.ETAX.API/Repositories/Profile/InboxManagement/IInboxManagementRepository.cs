namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IInboxManagementRepository
    {
        Task<Response> SEARCH_TODO(InboxSearchModel search);
        Task<Response> SEARCH_INPROGRESS(InboxSearchModel search);
        Task<Response> SEARCH_ALL(InboxSearchModel search);
    }
}
