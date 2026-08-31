export function CreateRequirementPage() {
  return (
    <div className="mx-auto max-w-3xl space-y-6">
      <div className="flex items-center justify-between gap-3">
        <div>
          <p className="text-sm font-medium text-muted-foreground">New ticket</p>
          <h2 className="text-3xl font-bold tracking-tight">Create requirement</h2>
        </div>
        <button
          type="button"
          onClick={() => window.location.assign('/requirements')}
          className="rounded-md border px-4 py-2 text-sm font-medium text-foreground transition-colors hover:bg-accent"
        >
          Back to list
        </button>
      </div>

      <form className="space-y-6 rounded-xl border bg-card p-6 shadow-sm">
        <div className="grid gap-5 md:grid-cols-2">
          <div className="space-y-2">
            <label className="text-sm font-medium" htmlFor="title">
              Requirement title
            </label>
            <input
              id="title"
              className="w-full rounded-md border bg-background px-3 py-2 text-sm outline-none ring-0 focus:border-ring"
              placeholder="Enter a concise title"
            />
          </div>

          <div className="space-y-2">
            <label className="text-sm font-medium" htmlFor="owner">
              Owner
            </label>
            <input
              id="owner"
              className="w-full rounded-md border bg-background px-3 py-2 text-sm outline-none ring-0 focus:border-ring"
              placeholder="Who owns this ticket?"
            />
          </div>
        </div>

        <div className="space-y-2">
          <label className="text-sm font-medium" htmlFor="summary">
            Summary
          </label>
          <textarea
            id="summary"
            rows={5}
            className="w-full rounded-md border bg-background px-3 py-2 text-sm outline-none ring-0 focus:border-ring"
            placeholder="Describe the need, user impact, and acceptance criteria"
          />
        </div>

        <div className="grid gap-5 md:grid-cols-3">
          <div className="space-y-2">
            <label className="text-sm font-medium" htmlFor="priority">
              Priority
            </label>
            <select
              id="priority"
              className="w-full rounded-md border bg-background px-3 py-2 text-sm outline-none ring-0 focus:border-ring"
              defaultValue="Medium"
            >
              <option value="Low">Low</option>
              <option value="Medium">Medium</option>
              <option value="High">High</option>
              <option value="Critical">Critical</option>
            </select>
          </div>

          <div className="space-y-2">
            <label className="text-sm font-medium" htmlFor="status">
              Status
            </label>
            <select
              id="status"
              className="w-full rounded-md border bg-background px-3 py-2 text-sm outline-none ring-0 focus:border-ring"
              defaultValue="New"
            >
              <option value="New">New</option>
              <option value="Draft">Draft</option>
              <option value="Approved">Approved</option>
              <option value="Blocked">Blocked</option>
            </select>
          </div>

          <div className="space-y-2">
            <label className="text-sm font-medium" htmlFor="dueDate">
              Due date
            </label>
            <input
              id="dueDate"
              type="date"
              className="w-full rounded-md border bg-background px-3 py-2 text-sm outline-none ring-0 focus:border-ring"
            />
          </div>
        </div>

        <div className="flex justify-end gap-3">
          <button
            type="button"
            onClick={() => window.location.assign('/requirements')}
            className="rounded-md border px-4 py-2 text-sm font-medium text-foreground transition-colors hover:bg-accent"
          >
            Cancel
          </button>
          <button
            type="submit"
            className="rounded-md bg-primary px-4 py-2 text-sm font-medium text-primary-foreground shadow-sm transition-colors hover:bg-primary/90"
          >
            Save requirement
          </button>
        </div>
      </form>
    </div>
  );
}
