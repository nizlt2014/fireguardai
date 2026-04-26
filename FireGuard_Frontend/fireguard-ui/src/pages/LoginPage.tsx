import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/client";
import { useAuth } from "../auth/AuthContext";

export default function LoginPage() {
  const [email, setEmail] = useState("admin@fireguard.com");
  const [password, setPassword] = useState("Admin@123");
  const [error, setError] = useState("");

  const { login } = useAuth();
  const navigate = useNavigate();

  const submit = async () => {
    try {
      const res = await api.post("/auth/login", {
        email,
        password
      });

      login(res.data.token);
      navigate("/");
    } catch {
      setError("Invalid login");
    }
  };

  return (
    <div className="min-h-screen bg-slate-950 text-white flex items-center justify-center">
      <div className="w-full max-w-md rounded-3xl bg-white/5 border border-white/10 p-8 space-y-5">

        <h1 className="text-4xl font-bold">
          FireGuard Login
        </h1>

        <input
          className="w-full p-3 rounded bg-slate-800"
          value={email}
          onChange={e => setEmail(e.target.value)}
        />

        <input
          type="password"
          className="w-full p-3 rounded bg-slate-800"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />

        {error && (
          <div className="text-red-400">
            {error}
          </div>
        )}

        <button
          onClick={submit}
          className="w-full bg-red-500 p-3 rounded-xl font-semibold"
        >
          Login
        </button>

      </div>
    </div>
  );
}