import { useEffect, useState } from "react";
import api from "../api/client";

export default function AIInsightsPage() {
    const [data, setData] = useState<any>(null);

    useEffect(() => {
        api.get("/aiinsights")
            .then(res => setData(res.data))
            .catch(err => {
                console.error("Failed to load AI insights", err);
            });
    }, []);

    if (!data) {
        return (
            <div className="text-white text-xl">
                Loading AI insights...
            </div>
        );
    }

    return (
        <div className="space-y-8">

            {/* Header */}
            <div>
                <p className="text-sm text-slate-400">
                    FireGuard Intelligence
                </p>

                <h1 className="text-5xl font-bold mt-1">
                    AI Insights
                </h1>
            </div>

            {/* KPI Cards */}
            <div className="grid md:grid-cols-2 xl:grid-cols-4 gap-5">

                <Card
                    title="Revenue Growth Potential"
                    value={data.revenueGrowthPotential}
                />

                <Card
                    title="Renewals At Risk"
                    value={data.renewalsAtRisk}
                />

                <Card
                    title="Overloaded Technicians"
                    value={data.overloadedTechnicians}
                />

                <Card
                    title="Upsell Opportunities"
                    value={data.upsellOpportunities}
                />

            </div>

            {/* Recommendations */}
            <div className="rounded-3xl p-6 bg-white/5 border border-white/10">
                <h2 className="text-2xl font-bold">
                    ✨ Recommended Actions
                </h2>

                <div className="mt-5 space-y-3">

                    {data.recommendations.map(
                        (item: string, index: number) => (
                            <div
                                key={index}
                                className="p-4 rounded-2xl bg-black/20"
                            >
                                {item}
                            </div>
                        )
                    )}

                </div>
            </div>

            {/* Summary */}
            <div className="rounded-3xl p-6 bg-gradient-to-r from-red-500/20 to-orange-500/20 border border-white/10">
                <h2 className="text-2xl font-bold">
                    🤖 Executive Summary
                </h2>

                <p className="mt-4 text-lg text-slate-100 leading-8 font-medium">
                    {data.summary}
                </p>
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
            <div className="text-sm text-slate-400">
                {title}
            </div>

            <div className="text-4xl font-bold mt-3">
                {value}
            </div>
        </div>
    );
}