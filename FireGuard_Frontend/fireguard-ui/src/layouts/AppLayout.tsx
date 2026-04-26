import { Link, Outlet, useLocation } from "react-router-dom";
import { getUserFromToken } from "../auth/token";

export default function AppLayout() {
    const location = useLocation();
    const user = getUserFromToken();

    const nav = [
        { to: "/", label: "Dashboard", icon: "📊" },
        { to: "/sites", label: "Sites", icon: "🏢" },
        { to: "/monitoring", label: "Monitoring", icon: "📡" },
        { to: "/technicians", label: "Technicians", icon: "👷" },
        { to: "/ai", label: "AI Insights", icon: "✨" }
    ];

    return (
        <div className="min-h-screen bg-slate-950 text-white flex">

            <aside className="w-72 bg-slate-900 border-r border-white/10 p-5 flex flex-col h-screen sticky top-0 overflow-y-auto">

                <div>
                    <h1 className="text-3xl font-bold">FireGuard</h1>
                    <p className="text-slate-400 text-sm mt-1">
                        AI Fire Safety Platform
                    </p>
                </div>

                <div className="space-y-2 mt-8">
                    {nav.map(item => {
                        const active = location.pathname === item.to;

                        return (
                            <Link
                                key={item.to}
                                to={item.to}
                                className={`block px-4 py-3 rounded-2xl transition ${active
                                        ? "bg-red-500 text-white"
                                        : "bg-white/5 hover:bg-white/10 text-slate-300"
                                    }`}
                            >
                                {item.icon} {item.label}
                            </Link>
                        );
                    })}
                </div>

                <div className="mt-auto mb-2 p-4 rounded-2xl bg-white/5 border border-white/10">

                    <div className="text-sm text-slate-400">
                        Logged in as
                    </div>

                    <div className="font-semibold text-sm break-all mt-1">
                        {user?.email}
                    </div>

                    <div className="text-xs text-red-300 mt-1">
                        {user?.role}
                    </div>

                    <button
                        onClick={() => {
                            localStorage.removeItem("token");
                            window.location.href = "/login";
                        }}
                        className="mt-4 w-full rounded-xl bg-red-500 p-2 text-sm font-semibold"
                    >
                        Logout
                    </button>

                </div>

            </aside>

            <main className="flex-1 p-8">
                <Outlet />
            </main>

        </div>
    );
}