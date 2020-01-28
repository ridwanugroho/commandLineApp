using HtmlAgilityPack;

namespace IPGetterObj
{
    class IPGetter
    {
        HtmlDocument htmlPage;
        public IPGetter(string url)
        {
            HtmlWeb page = new HtmlWeb();
            htmlPage = page.Load(url);
        }

        public string GetPublicIP()
        {
            return htmlPage.DocumentNode.SelectSingleNode("//strong[@id='ip_address']").InnerText;
        }
    }
}