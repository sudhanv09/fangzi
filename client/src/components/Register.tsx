"use client";

import { useForm } from "@mantine/form";
import {
  TextInput,
  PasswordInput,
  Text,
  Paper,
  Group,
  PaperProps,
  Button,
  Title,
  Container,
  Anchor,
  Stack,
} from "@mantine/core";
import React from "react";
import classes from "./LoginForm.module.css";

export function RegisterForm(props: PaperProps) {
  const form = useForm({
    initialValues: {
      email: "",
      name: "",
      password: "",
      terms: true,
    },

    validate: {
      email: (val) => (/^\S+@\S+$/.test(val) ? null : "Invalid email"),
      password: (val) =>
        val.length <= 6
          ? "Password should include at least 6 characters"
          : null,
    },
  });

  return (
    <Container size={450} my={40}>
      <Title ta="center" className={classes.title}>
        Hi There!
      </Title>
      <Text ta="center" size="lg" fw={500} mt={5}>
        Register your account to continue.
      </Text>

      <Paper withBorder shadow="md" p={30} mt={30} radius="md" {...props}>
        <form onSubmit={form.onSubmit(() => {})}>
          <Stack>
            <TextInput
              label="Name"
              placeholder="Your name"
              value={form.values.name}
              onChange={(event) =>
                form.setFieldValue("name", event.currentTarget.value)
              }
              radius="md"
            />

            <TextInput
              required
              label="Email"
              placeholder="hello@mantine.dev"
              value={form.values.email}
              onChange={(event) =>
                form.setFieldValue("email", event.currentTarget.value)
              }
              error={form.errors.email && "Invalid email"}
              radius="md"
            />

            <PasswordInput
              required
              label="Password"
              placeholder="Your password"
              value={form.values.password}
              onChange={(event) =>
                form.setFieldValue("password", event.currentTarget.value)
              }
              error={
                form.errors.password &&
                "Password should include at least 6 characters"
              }
              radius="md"
            />

            <PasswordInput
              required
              label="Confirm Password"
              placeholder="Your password"
              value={form.values.password}
              onChange={(event) =>
                form.setFieldValue("password", event.currentTarget.value)
              }
              error={
                form.errors.password &&
                "Password should include at least 6 characters"
              }
              radius="md"
            />
          </Stack>

          <Group justify="end" mt="xl">
            <Button type="submit" radius="xl">
              Register
            </Button>
          </Group>
        </form>
      </Paper>
      <Text c="dimmed" size="sm" ta="center" mt={20}>
        Already have an account? <Anchor href="/login">Login</Anchor>
      </Text>
    </Container>
  );
}
