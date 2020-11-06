
using System.Collections.Generic;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class FileListResultModel
    {
        public List<FileModel> Files { get; private set; }

        public int Amount { get; private set; }

        public int Offset { get; private set; }

        public FileListResultModel(IEnumerable<File> files, int amount, int offset)
        {
            List<FileModel> ret = new List<FileModel>();

            foreach (var t in files)
            {
                ret.Add(t.ToVo());
            }

            this.Files = ret;
            this.Amount = ret.Count;
            this.Offset = offset;
        }
    }
}
