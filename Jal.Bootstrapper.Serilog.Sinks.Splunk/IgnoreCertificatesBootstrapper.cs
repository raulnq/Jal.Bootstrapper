using System;
using System.Linq;
using System.Net;
using Jal.Bootstrapper.Interface;

namespace Jal.Bootstrapper.Serilog.Sinks.Splunk
{
    public class IgnoreCertificatesBootstrapper : IBootstrapper<bool>
    {
        private readonly string _certificatesToIgnore;

        public IgnoreCertificatesBootstrapper(string certificatesToIgnore)
        {
            _certificatesToIgnore = certificatesToIgnore;
        }


        public void Configure()
        {
            var certificatesToIgnore = _certificatesToIgnore;

            if (!string.IsNullOrWhiteSpace(certificatesToIgnore))
            {
                var sites = certificatesToIgnore.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (!sites.Any())

                    return;

                var original = ServicePointManager.ServerCertificateValidationCallback;

                ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) =>
                {
                    var request = o as HttpWebRequest;

                    if (request != null && sites.Any(s => request.Address.Host.EndsWith(s)))

                        return true;

                    return original == null || original(o, certificate, chain, errors);
                };
            }

            Result = true;
        }

        public bool Result { get; set; }
    }
}
