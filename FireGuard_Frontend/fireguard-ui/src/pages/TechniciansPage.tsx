import { useEffect, useState } from "react";
import api from "../api/client";

export default function TechniciansPage() {
  const [data, setData] = useState<any>(null);

  useEffect(() => {
    api.get("/technicians")
      .then(res => setData(res.data))
      .catch(() => console.log("Failed to load technicians"));
  }, []);

  if (!data) {
    return (
      <div className="text-white text-xl">
        Loading technicians...
      </div>
    );
  }

  const badge = (status: string) => {
    if (status === "On Job")
      return "bg-red-500/20 text-red-300";

    if (status === "Travelling")
      return "bg-yellow-500/20 text-yellow-300";

    return "bg-emerald-500/20 text-emerald-300";
  };

  return (
    <div className="space-y-8">

      <div>
        <p className="text-sm text-slate-400">
          FireGuard Workforce
        </p>

        <h1 className="text-5xl font-bold mt-1">
          Technicians
        </h1>
      </div>

      {/* KPI */}
      <div className="grid md:grid-cols-2 xl:grid-cols-4 gap-5">

        <Card title="Total Technicians" value={data.totalTechnicians} />
        <Card title="Active Jobs" value={data.activeJobs} />
        <Card title="Available Now" value={data.availableNow} />
        <Card title="Avg ETA" value={`${data.avgEta}m`} />

      </div>

      {/* Cards */}
      <div className="grid xl:grid-cols-2 gap-6">

        {data.technicians.map((t: any) => (
          <div
            key={t.id}
            className="rounded-3xl p-6 bg-white/5 border border-white/10"
          >
            <div className="flex justify-between items-start">

              <div>
                <div className="text-2xl font-bold">
                  👷 {t.name}
                </div>

                <div className="text-slate-400 mt-1">
                  Assigned Site: {t.siteName}
                </div>

                <div className="text-slate-500 text-sm mt-1">
                  Skill: {t.skill}
                </div>
              </div>

              <span
                className={`px-3 py-1 rounded-full text-sm ${badge(t.status)}`}
              >
                {t.status}
              </span>

            </div>

            <div className="grid grid-cols-2 gap-4 mt-6">

              <MiniCard
                title="ETA"
                value={
                  t.etaMinutes > 0
                    ? `${t.etaMinutes}m`
                    : "-"
                }
              />

              <MiniCard
                title="Jobs Today"
                value={t.jobsToday}
              />

            </div>
          </div>
        ))}

      </div>

    </div>
  );
}

function Card({
  title,
  value
}: {
  title: string;
  value: any;
}) {
  return (
    <div className="p-6 rounded-3xl bg-white/5 border border-white/10">
      <div className="text-sm text-slate-400">{title}</div>
      <div className="text-4xl font-bold mt-3">{value}</div>
    </div>
  );
}

function MiniCard({
  title,
  value
}: {
  title: string;
  value: any;
}) {
  return (
    <div className="p-4 rounded-2xl bg-black/20">
      <div className="text-sm text-slate-400">{title}</div>
      <div className="text-xl font-bold mt-1">{value}</div>
    </div>
  );
}