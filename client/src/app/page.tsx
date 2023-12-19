"use client";

import Hero from "../components/Hero";
import Header from "../components/NavHeader";
import React from "react";
import { supabase } from "@/api/supabaseClient";

export default function Demo() {
  return (
    <div>
      <Header />
      <Hero />
    </div>
  );
}
