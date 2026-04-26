import { useEffect, useState } from "react";
import api from "../api/client";

interface DashboardStats {
  totalSites: number;
  monthlyRevenue: number;
  criticalAlerts: number;
  renewalsDue: number;
}

export default function Dashboard() {
  const [data, setData] = useState<DashboardStats | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.get("/dashboard")
      .then(res => setData(res.data))
      .catch(err => console.error(err))
      .finally(() => setLoading(false));
  }, []);

  if (loading) {
    return <div className="p-10 text-xl">Loading dashboard...</div>;
  }

  if (!data) {
    return <div className="p-10 text-red-400">Failed to load dashboard.</div>;
  }

  return (
    <div className="min-h-screen bg-slate-950 text-white p-8">
      <div className="max-w-7xl mx-auto space-y-8">

        <div>
          <p className="text-slate-400 text-sm">FireGuard Live</p>
          <h1 className="text-5xl font-bold mt-1">Dashboard</h1>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-4 gap-5">

          <Card title="Client Sites" value={data.totalSites.toString()} icon="🏢" />
          <Card title="Monthly Revenue" value={`₹${data.monthlyRevenue}`} icon="💰" />
          <Card title="Critical Alerts" value={data.criticalAlerts.toString()} icon="🚨" />
          <Card title="Renewals Due" value={data.renewalsDue.toString()} icon="📄" />

        </div>

        <div className="rounded-3xl border border-white/10 bg-white/5 p-6">
          <p className="text-red-300 text-sm">AI Forecast</p>
          <h2 className="text-3xl font-bold mt-2">
            Revenue Growth Expected This Month
          </h2>
          <p className="text-slate-300 mt-2">
            Based on live renewals and site performance.
          </p>
        </div>

      </div>
    </div>
  );
}

function Card({
  title,
  value,
  icon
}: {
  title: string;
  value: string;
  icon: string;
}) {
  return (
    <div className="rounded-3xl border border-white/10 bg-white/5 p-5">
      <div className="flex justify-between">
        <span className="text-slate-400 text-sm">{title}</span>
        <span>{icon}</span>
      </div>

      <div className="text-4xl font-bold mt-4">{value}</div>
    </div>
  );
}