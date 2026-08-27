import type { ReactNode } from "react";
import type { ChangeTicket } from "../types";
import { PriorityBadge, StatusBadge, TypeBadge } from "./Badges";

function formatDate(value: string | null): string {
  if (!value) return "—";
  return new Intl.DateTimeFormat(undefined, {
    dateStyle: "medium",
    timeStyle: "short"
  }).format(new Date(value));
}

function Section({ title, children }: { title: string; children: ReactNode }) {
  return (
    <section className="rounded-xl border border-slate-200 bg-white p-4">
      <h3 className="mb-3 text-sm font-semibold uppercase tracking-wide text-slate-500">{title}</h3>
      {children}
    </section>
  );
}

export function TicketDetail({ ticket }: { ticket: ChangeTicket }) {
  const emails = ticket.attachments.filter((item) => item.kind === "Email");
  const screenshots = ticket.attachments.filter((item) => item.kind === "Screenshot");

  return (
    <div className="space-y-4">
      <header className="rounded-xl border border-slate-200 bg-white p-5">
        <div className="flex flex-wrap items-start justify-between gap-3">
          <div>
            <h2 className="text-xl font-semibold text-slate-900">{ticket.title}</h2>
            <p className="mt-2 max-w-3xl text-sm leading-6 text-slate-600">{ticket.description}</p>
          </div>
          <div className="flex flex-wrap gap-2">
            <TypeBadge value={ticket.changeType} />
            <StatusBadge value={ticket.status} />
            <PriorityBadge value={ticket.priority} />
          </div>
        </div>
        <dl className="mt-5 grid grid-cols-1 gap-4 text-sm sm:grid-cols-2 lg:grid-cols-4">
          <div>
            <dt className="text-slate-500">Requester</dt>
            <dd className="font-medium">{ticket.requester}</dd>
          </div>
          <div>
            <dt className="text-slate-500">Assigned to</dt>
            <dd className="font-medium">{ticket.assignedTo ?? "Unassigned"}</dd>
          </div>
          <div>
            <dt className="text-slate-500">Scheduled start</dt>
            <dd className="font-medium">{formatDate(ticket.scheduledStart)}</dd>
          </div>
          <div>
            <dt className="text-slate-500">Scheduled end</dt>
            <dd className="font-medium">{formatDate(ticket.scheduledEnd)}</dd>
          </div>
        </dl>
      </header>

      <div className="grid gap-4 lg:grid-cols-2">
        <Section title="Implementation plan">
          <p className="text-sm leading-6 text-slate-700">{ticket.implementationPlan ?? "None recorded."}</p>
        </Section>
        <Section title="Rollback plan">
          <p className="text-sm leading-6 text-slate-700">{ticket.rollbackPlan ?? "None recorded."}</p>
        </Section>
      </div>

      <Section title="Change log">
        {ticket.changeLog.length === 0 ? (
          <p className="text-sm text-slate-500">No change log comments yet.</p>
        ) : (
          <ol className="space-y-3">
            {ticket.changeLog.map((entry) => (
              <li key={entry.id} className="rounded-lg bg-slate-50 px-3 py-2">
                <div className="flex items-center justify-between gap-3 text-xs text-slate-500">
                  <span className="font-medium text-slate-700">{entry.author}</span>
                  <time>{formatDate(entry.createdAt)}</time>
                </div>
                <p className="mt-1 text-sm text-slate-800">{entry.body}</p>
              </li>
            ))}
          </ol>
        )}
      </Section>

      <div className="grid gap-4 lg:grid-cols-2">
        <Section title="Email attachments">
          {emails.length === 0 ? (
            <p className="text-sm text-slate-500">No emails attached.</p>
          ) : (
            <ul className="space-y-2 text-sm">
              {emails.map((item) => (
                <li key={item.id} className="rounded-lg border border-slate-200 px-3 py-2">
                  <p className="font-medium">{item.fileName}</p>
                  <p className="text-slate-500">{item.emailSubject ?? "No subject"}</p>
                  <p className="text-xs text-slate-400">{item.emailFrom ?? "Unknown sender"}</p>
                </li>
              ))}
            </ul>
          )}
        </Section>
        <Section title="Screenshots">
          {screenshots.length === 0 ? (
            <p className="text-sm text-slate-500">No screenshots attached.</p>
          ) : (
            <ul className="space-y-2 text-sm">
              {screenshots.map((item) => (
                <li key={item.id} className="rounded-lg border border-slate-200 px-3 py-2">
                  <p className="font-medium">{item.fileName}</p>
                  <p className="text-xs text-slate-500">{item.contentType}</p>
                </li>
              ))}
            </ul>
          )}
        </Section>
      </div>
    </div>
  );
}
