import { useEffect, useState } from "react";
import { getSites, createSite, deleteSite } from "../api/sitesApi";

export default function SitesPage() {
  const [sites, setSites] = useState<any[]>([]);
  const [name, setName] = useState("");
  const [city, setCity] = useState("");

  const load = async () => {
    const data = await getSites();
    setSites(data);
  };

  useEffect(() => {
    load();
  }, []);

  const add = async () => {
    await createSite({
      name,
      city,
      status: "Healthy",
      monthlyRevenue: 25000
    });

    setName("");
    setCity("");
    load();
  };

  const remove = async (id: number) => {
    await deleteSite(id);
    load();
  };

  return (
    <div className="min-h-screen bg-slate-950 text-white p-8">
      <div className="max-w-6xl mx-auto space-y-6">

        <h1 className="text-4xl font-bold">Sites</h1>

        <div className="grid md:grid-cols-3 gap-3">
          <input
            className="p-3 rounded bg-slate-800"
            placeholder="Site Name"
            value={name}
            onChange={e => setName(e.target.value)}
          />

          <input
            className="p-3 rounded bg-slate-800"
            placeholder="City"
            value={city}
            onChange={e => setCity(e.target.value)}
          />

          <button
            onClick={add}
            className="bg-red-500 rounded p-3"
          >
            + Add Site
          </button>
        </div>

        <div className="space-y-3">
          {sites.map(site => (
            <div
              key={site.id}
              className="p-4 rounded bg-slate-900 flex justify-between items-center"
            >
              <div>
                <div className="font-semibold">{site.name}</div>
                <div className="text-slate-400 text-sm">
                  {site.city} • ₹{site.monthlyRevenue}
                </div>
              </div>

              <button
                onClick={() => remove(site.id)}
                className="bg-red-600 px-3 py-1 rounded"
              >
                Delete
              </button>
            </div>
          ))}
        </div>

      </div>
    </div>
  );
}