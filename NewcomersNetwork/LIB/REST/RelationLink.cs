using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewcomersNetworkAPI.LIB
{
    public class RelationLink
    {
        public readonly string rel;
        public readonly string uri;
        public readonly RESTHttpMethod method;

        public RelationLink(string rel, string uri, RESTHttpMethod method)
        {
            this.method = method;
            this.rel = rel;
            this.uri = uri;
        }
    }
}
