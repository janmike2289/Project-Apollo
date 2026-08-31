export function RequirementsPage() {
  const sections = [
    {
      title: "Priority backlog",
      items: [
        { id: "REQ-104", label: "Implement role-based access control", priority: "High" },
        { id: "REQ-118", label: "Improve intake form validation", priority: "High" },
        { id: "REQ-129", label: "Add audit trail for approvals", priority: "Medium" },
      ],
    },
    {
      title: "Approved",
      items: [
        { id: "REQ-092", label: "Standardize document naming", priority: "Low" },
        { id: "REQ-101", label: "Add customer notification preferences", priority: "Medium" },
      ],
    },
    {
      title: "Needs clarification",
      items: [
        { id: "REQ-135", label: "Define offline sync expectations", priority: "Medium" },
      ],
    },
  ];

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between gap-3">
        <div>
          <p className="text-sm font-medium text-muted-foreground">Portfolio</p>
          <h2 className="text-3xl font-bold tracking-tight">Requirements Master</h2>
        </div>
        <button
          type="button"
          onClick={() => window.location.assign('/create-requirement')}
          className="rounded-md border px-4 py-2 text-sm font-medium text-foreground transition-colors hover:bg-accent"
        >
          Add requirement
        </button>
      </div>

      <div className="grid gap-6 xl:grid-cols-3">
        {sections.map((section) => (
          <section key={section.title} className="rounded-xl border bg-card p-5 shadow-sm">
            <h3 className="mb-4 text-lg font-semibold">{section.title}</h3>

            <div className="space-y-3">
              {section.items.map((item) => (
                <div key={item.id} className="rounded-lg border p-3">
                  <div className="flex items-center justify-between gap-2">
                    <span className="text-xs font-medium uppercase tracking-wide text-muted-foreground">{item.id}</span>
                    <span
                      className={
                        item.priority === "High"
                          ? "rounded-full bg-red-100 px-2 py-0.5 text-[10px] font-medium text-red-700"
                          : item.priority === "Medium"
                            ? "rounded-full bg-amber-100 px-2 py-0.5 text-[10px] font-medium text-amber-700"
                            : "rounded-full bg-slate-200 px-2 py-0.5 text-[10px] font-medium text-slate-700"
                      }
                    >
                      {item.priority}
                    </span>
                  </div>
                  <p className="mt-2 font-medium">{item.label}</p>
                </div>
              ))}
            </div>
          </section>
        ))}
      </div>
    </div>
  );
}
