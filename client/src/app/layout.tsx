import React from "react";
import "@mantine/core/styles.css";
import { MantineProvider, ColorSchemeScript } from "@mantine/core";
import {
  QueryClient,
  QueryClientProvider,
} from "react-query";

export const metadata = {
  title: "Fangzi",
  description: "A simple rental search website.",
};

const queryClient = new QueryClient();

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <head>
        <ColorSchemeScript />
      </head>
      <body>
        <MantineProvider>
          <QueryClientProvider client={queryClient}>
            {children}
          </QueryClientProvider>
        </MantineProvider>
      </body>
    </html>
  );
}
