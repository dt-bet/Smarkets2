using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Entity.XML
{
    public class MatchTeam
    {
        public long EventId { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public TeamClass Type { get; set; }

    }
}
//      "event_id": "41607433",
//      "id": "63849",
//      "info": {},
//      "name": "Nik Razborsek",
//      "short_code": null,
//      "short_name": "Nik Razborsek",
//      "slug": "nik-razborsek",
//      "type": "b"