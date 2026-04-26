import {
  BrowserRouter,
  Routes,
  Route
} from "react-router-dom";
import MonitoringPage from "./pages/MonitoringPage";
import { AuthProvider } from "./auth/AuthContext";
import ProtectedRoute from "./auth/ProtectedRoute";

import AppLayout from "./layouts/AppLayout";

import LoginPage from "./pages/LoginPage";
import DashboardPage from "./pages/Dashboard";
import SitesPage from "./pages/SitesPage";
import TechniciansPage from "./pages/TechniciansPage";
import AIInsightsPage from "./pages/AIInsightsPage";

export default function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>

          {/* Public */}
          <Route path="/login" element={<LoginPage />} />

          {/* Protected App Shell */}
          <Route
            element={
              <ProtectedRoute>
                <AppLayout />
              </ProtectedRoute>
            }
          >
            <Route path="/" element={<DashboardPage />} />
            <Route path="/sites" element={<SitesPage />} />
            <Route path="/monitoring" element={<MonitoringPage />} />
            <Route path="/technicians" element={<TechniciansPage />} />
            <Route path="/ai" element={<AIInsightsPage />} />
          </Route>

        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}