namespace Blazorcrud.Client.Shared
{
    public class PageHistoryState
    {
        private List<string> previousPages;

        public PageHistoryState()
        {
            previousPages = new List<string>();
        }

        public void AddPageToHistory(string PageName)
        {
            previousPages.Add(PageName);
        }

        public string GetGoBackPage()
        {
            if (previousPages.Count > 1)
            {
                // page added on initialization, return second from last
                return previousPages.ElementAt(previousPages.Count - 1);
            }
            // can't go back page
            return previousPages.FirstOrDefault();
        }

        public bool CanGoBack()
        {
            return previousPages.Count > 1;
        }
    }
}