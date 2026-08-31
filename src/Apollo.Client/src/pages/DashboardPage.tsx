export function DashboardPage() {
  const stats = [
    { label: "Open requirements", value: "128", change: "+12%" },
    { label: "Approved this week", value: "24", change: "+5%" },
    { label: "Blocked items", value: "8", change: "-2" },
    { label: "Avg. review time", value: "4.2d", change: "-0.8d" },
  ];

  const projects = [
    { name: "Apollo Platform", status: "On track", owner: "Product Ops" },
    { name: "Client Portal", status: "At risk", owner: "UX Team" },
    { name: "API Modernization", status: "On track", owner: "Engineering" },
  ];

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between gap-3">
        <div>
          <p className="text-sm font-medium text-muted-foreground">Overview</p>
          <h2 className="text-3xl font-bold tracking-tight">Dashboard</h2>
        </div>
        <button className="rounded-md bg-primary px-4 py-2 text-sm font-medium text-primary-foreground shadow-sm transition-colors hover:bg-primary/90">
          New review
        </button>
      </div>

      <div className="grid gap-4 md:grid-cols-2 xl:grid-cols-4">
        {stats.map((stat) => (
          <div key={stat.label} className="rounded-xl border bg-card p-4 shadow-sm">
            <p className="text-sm text-muted-foreground">{stat.label}</p>
            <div className="mt-3 flex items-end justify-between gap-2">
              <span className="text-3xl font-semibold">{stat.value}</span>
              <span className="text-sm font-medium text-emerald-600">{stat.change}</span>
            </div>
          </div>
        ))}
      </div>

      <div className="grid gap-6 xl:grid-cols-[1.5fr_1fr]">
        <section className="rounded-xl border bg-card p-5 shadow-sm">
          <div className="mb-4 flex items-center justify-between">
            <h3 className="text-lg font-semibold">Active initiatives</h3>
            <span className="text-sm text-muted-foreground">Last 30 days</span>
          </div>

          <div className="space-y-4">
            {projects.map((project) => (
              <div key={project.name} className="rounded-lg border p-4">
                <div className="flex items-center justify-between gap-3">
                  <div>
                    <p className="font-medium">{project.name}</p>
                    <p className="text-sm text-muted-foreground">Owner: {project.owner}</p>
                  </div>
                  <span
                    className={
                      project.status === "On track"
                        ? "rounded-full bg-emerald-100 px-2.5 py-1 text-xs font-medium text-emerald-700"
                        : "rounded-full bg-amber-100 px-2.5 py-1 text-xs font-medium text-amber-700"
                    }
                  >
                    {project.status}
                  </span>
                </div>
              </div>
            ))}
          </div>
        </section>

        <aside className="rounded-xl border bg-card p-5 shadow-sm">
          <h3 className="text-lg font-semibold">Upcoming review</h3>
          <div className="mt-4 space-y-4">
            <div className="rounded-lg bg-muted p-3">
              <p className="text-sm text-muted-foreground">Mon, Sep 2</p>
              <p className="mt-1 font-medium">Requirements sign-off</p>
            </div>
            <div className="rounded-lg bg-muted p-3">
              <p className="text-sm text-muted-foreground">Wed, Sep 4</p>
              <p className="mt-1 font-medium">Scope validation</p>
            </div>
            <div className="rounded-lg bg-muted p-3">
              <p className="text-sm text-muted-foreground">Fri, Sep 6</p>
              <p className="mt-1 font-medium">Stakeholder approvals</p>
            </div>
          </div>
        </aside>
      </div>
    </div>
  );
}
