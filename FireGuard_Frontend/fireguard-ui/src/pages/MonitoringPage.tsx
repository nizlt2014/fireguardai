import { useEffect, useState } from "react";
import api from "../api/client";

export default function MonitoringPage() {
  const [data, setData] = useState<any>(null);

  useEffect(() => {
    api.get("/monitoring")
      .then(res => setData(res.data))
      .catch(() => console.log("Failed to load monitoring"));
  }, []);

  if (!data) {
    return (
      <div className="text-white text-xl">
        Loading monitoring...
      </div>
    );
  }

  const badge = (status: string) => {
    if (status === "Critical")
      return "bg-red-500/20 text-red-300";

    if (status === "Warning")
      return "bg-yellow-500/20 text-yellow-300";

    return "bg-emerald-500/20 text-emerald-300";
  };

  return (
    <div className="space-y-8">

      <div>
        <p className="text-sm text-slate-400">
          FireGuard Live
        </p>

        <h1 className="text-5xl font-bold mt-1">
          Monitoring Center
        </h1>
      </div>

      {/* KPI */}
      <div className="grid md:grid-cols-2 xl:grid-cols-4 gap-5">

        <Card title="Online Devices" value={data.onlineDevices} />
        <Card title="Warnings" value={data.warnings} />
        <Card title="Critical" value={data.critical} />
        <Card title="Sites Connected" value={data.sitesConnected} />

      </div>

      {/* Devices */}
      <div className="rounded-3xl bg-white/5 border border-white/10 p-6">
        <h2 className="text-2xl font-bold">
          📡 Live Devices
        </h2>

        <div className="mt-6 space-y-3">

          {data.devices.map((item: any) => (
            <div
              key={item.id}
              className="grid grid-cols-6 gap-4 p-4 rounded-2xl bg-black/20 items-center"
            >
              <div>{item.siteName}</div>
              <div>{item.deviceName}</div>
              <div>{item.deviceType}</div>

              <div>
                <span
                  className={`px-3 py-1 rounded-full text-sm ${badge(item.status)}`}
                >
                  {item.status}
                </span>
              </div>

              <div className="text-sm text-slate-400">
                {item.batteryLevel}%
              </div>

              <div className="text-sm text-slate-400">
                {new Date(item.lastSeen).toLocaleTimeString()}
              </div>
            </div>
          ))}

        </div>
      </div>

    </div>
  );
}

function Card({
  title,
  value
}: {
  title: string;
  value: number;
}) {
  return (
    <div className="p-6 rounded-3xl bg-white/5 border border-white/10">
      <div className="text-sm text-slate-400">
        {title}
      </div>

      <div className="text-4xl font-bold mt-3">
        {value}
      </div>
    </div>
  );
}