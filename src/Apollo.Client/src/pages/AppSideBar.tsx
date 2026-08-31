import { CircleGauge, BookOpenCheck } from "lucide-react";
import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
} from "@/components/ui/sidebar";

// Define strict types for menu items
interface NavigationItem {
  title: string;
  url: string;
  icon: React.ComponentType<{ className?: string }>;
}

const mainNavItems: NavigationItem[] = [
  { title: "Dashboard", url: "/dashboard", icon: CircleGauge },
  { title: "Requirements Master", url: "/requirements", icon: BookOpenCheck },
];

export function AppSidebar() {
  return (
    <Sidebar variant="sidebar" collapsible="icon">
      {/* Sidebar Header / Branding */}
      <SidebarHeader className="border-b px-6 py-4">
        <div className="flex items-center gap-2 font-semibold">
          <span className="h-6 w-6 rounded-md bg-primary" />
          <span className="group-data-[collapsible=icon]:hidden">Requirments Master</span>
        </div>
      </SidebarHeader>

      <SidebarContent>
        <SidebarGroup>
          {/* <SidebarGroupLabel>Application</SidebarGroupLabel> */}
          <SidebarGroupContent>
            <SidebarMenu>
              {mainNavItems.map((item) => (
                <SidebarMenuItem key={item.title}>
                  <SidebarMenuButton
                    tooltip={item.title}
                    onClick={() => window.location.assign(item.url)}
                    className="w-full justify-start">
                    <item.icon className="h-4 w-4" />
                    <span>{item.title}</span>
                  </SidebarMenuButton>
                </SidebarMenuItem>
              ))}
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
    </Sidebar>
  );
}