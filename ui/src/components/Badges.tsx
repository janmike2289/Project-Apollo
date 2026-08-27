import type { ChangePriority, ChangeStatus, ChangeType } from "../types";

const statusStyles: Record<ChangeStatus, string> = {
  Draft: "bg-slate-100 text-slate-700",
  Submitted: "bg-sky-100 text-sky-800",
  Approved: "bg-emerald-100 text-emerald-800",
  Scheduled: "bg-indigo-100 text-indigo-800",
  InProgress: "bg-amber-100 text-amber-800",
  Completed: "bg-teal-100 text-teal-800",
  Rejected: "bg-rose-100 text-rose-800",
  Cancelled: "bg-zinc-200 text-zinc-700"
};

const priorityStyles: Record<ChangePriority, string> = {
  Low: "bg-slate-100 text-slate-700",
  Medium: "bg-blue-100 text-blue-800",
  High: "bg-orange-100 text-orange-800",
  Critical: "bg-red-100 text-red-800"
};

const typeStyles: Record<ChangeType, string> = {
  Standard: "bg-slate-100 text-slate-700",
  Normal: "bg-violet-100 text-violet-800",
  Emergency: "bg-red-100 text-red-800"
};

function Badge({ className, children }: { className: string; children: string }) {
  return (
    <span className={`inline-flex rounded-full px-2 py-0.5 text-xs font-semibold ${className}`}>
      {children}
    </span>
  );
}

export function StatusBadge({ value }: { value: ChangeStatus }) {
  return <Badge className={statusStyles[value]}>{value}</Badge>;
}

export function PriorityBadge({ value }: { value: ChangePriority }) {
  return <Badge className={priorityStyles[value]}>{value}</Badge>;
}

export function TypeBadge({ value }: { value: ChangeType }) {
  return <Badge className={typeStyles[value]}>{value}</Badge>;
}
