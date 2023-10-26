using System.Collections.Generic;

namespace TestAPI.Helper
{
    public class MessageInfo
    {
        public bool Status { get; set; } = false;
        public List<string> Message { get; set; } = new List<string>();
    }

    public class AllMessage
    {
        public string SUCCESS_INSERT = "Data berhasil disimpan";
        public string SUCCESS_UPDATE = "Data berhasil diperbaruhi";
        public string SUCCESS_DELETE = "Data berhasil dihapus";
        public string DATA_NOTFOUND = "Data berhasil diperbaruhi";
        public string FAIL_DELETE = "Data gagal dihapus";
        public string FAIL_UPDATE = "Data gagal diperbaruhi";
        public string FAIL_INSERT = "Data gagal disimpan";
    }
}