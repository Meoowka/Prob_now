using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prob_now.Entities;

namespace Prob_now.Interfaces
{
    public interface IModulesProvider
    {
        public Module[] ListModules();
        public FtpConnectionHandler GetFtp();
        public Module GetModule(Guid id);
        public Module[] Listing_ftp();
      
    }
}
