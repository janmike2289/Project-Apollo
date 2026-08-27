import type { ChangeTicket } from "../types";
import { PriorityBadge, StatusBadge, TypeBadge } from "./Badges";

interface TicketListProps {
  tickets: ChangeTicket[];
  selectedId: string | null;
  onSelect: (id: string) => void;
}

export function TicketList({ tickets, selectedId, onSelect }: TicketListProps) {
  if (tickets.length === 0) {
    return (
      <div className="px-4 py-10 text-center text-sm text-slate-500">
        No change tickets match the current filters.
      </div>
    );
  }

  return (
    <ul className="divide-y divide-slate-200">
      {tickets.map((ticket) => {
        const selected = ticket.id === selectedId;
        return (
          <li key={ticket.id}>
            <button
              type="button"
              onClick={() => onSelect(ticket.id)}
              className={`w-full px-4 py-3 text-left transition ${
                selected ? "bg-indigo-50" : "hover:bg-slate-50"
              }`}
            >
              <div className="flex items-start justify-between gap-3">
                <p className="font-medium text-slate-900">{ticket.title}</p>
                <StatusBadge value={ticket.status} />
              </div>
              <div className="mt-2 flex flex-wrap gap-2">
                <TypeBadge value={ticket.changeType} />
                <PriorityBadge value={ticket.priority} />
              </div>
              <p className="mt-2 text-xs text-slate-500">
                {ticket.requester}
                {ticket.assignedTo ? ` → ${ticket.assignedTo}` : ""}
              </p>
            </button>
          </li>
        );
      })}
    </ul>
  );
}
