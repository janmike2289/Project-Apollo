import './App.css';
import { SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar";
import { AppSidebar } from "@/pages/AppSideBar";
import { DashboardPage } from "@/pages/DashboardPage";
import { RequirementsPage } from "@/pages/RequirementsPage";

const currentPage = window.location.pathname === '/requirements' ? 'requirements' : 'dashboard';

export default function App() {
  const page = currentPage;

  return (
    <SidebarProvider>
      <div className="flex min-h-screen w-screen bg-background text-foreground">
        <AppSidebar />

        <main className="flex-1 flex flex-col min-h-screen">
          <header className="flex h-14 items-center gap-4 border-b px-6 bg-card">
            <SidebarTrigger />
          </header>

          <div className="flex-1 p-6 overflow-y-auto">
            {page === 'requirements' ? <RequirementsPage /> : <DashboardPage />}
          </div>
        </main>
      </div>
    </SidebarProvider>
  );
}